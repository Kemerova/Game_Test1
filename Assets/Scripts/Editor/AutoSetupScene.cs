#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class AutoSetupScene
{
    [MenuItem("SurvivorLite/Auto Setup Complete Scene")]
    public static void SetupCompleteScene()
    {
        // Clear existing scene
        var existingObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (var obj in existingObjects)
        {
            if (obj.name != "Main Camera")
                GameObject.DestroyImmediate(obj);
        }

        // Setup Camera
        var camera = Camera.main;
        if (camera == null)
        {
            var cameraGO = new GameObject("Main Camera");
            camera = cameraGO.AddComponent<Camera>();
            cameraGO.AddComponent<AudioListener>();
            cameraGO.tag = "MainCamera";
        }
        camera.orthographic = true;
        camera.orthographicSize = 10;
        camera.backgroundColor = new Color(0.1f, 0.1f, 0.2f);
        
        var cameraFollow = camera.GetComponent<CameraFollow2D>();
        if (!cameraFollow) cameraFollow = camera.gameObject.AddComponent<CameraFollow2D>();

        // Create Background
        var background = CreateSprite("Background", Vector3.zero, new Vector3(40, 40, 1));
        var bgRenderer = background.GetComponent<SpriteRenderer>();
        bgRenderer.color = new Color(0.05f, 0.1f, 0.05f);
        bgRenderer.sortingOrder = -10;

        // Create Player
        var player = CreateSprite("Player", Vector3.zero, Vector3.one);
        player.tag = "Player";
        var playerRenderer = player.GetComponent<SpriteRenderer>();
        playerRenderer.color = new Color(0.2f, 0.8f, 0.2f);
        
        // Player components
        var rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        
        var collider = player.AddComponent<CircleCollider2D>();
        collider.radius = 0.4f;
        
        player.AddComponent<PlayerController2D>();
        var health = player.AddComponent<Health>();
        health.maxHP = 100;
        var experience = player.AddComponent<Experience>();
        var weaponSystem = player.AddComponent<WeaponSystem>();

        // Set camera target
        cameraFollow.target = player.transform;

        // Create Object Pools
        var projectilePool = CreatePool("ProjectilePool", CreateProjectilePrefab());
        var enemyPool = CreatePool("EnemyPool", CreateEnemyPrefab());
        var xpPool = CreatePool("XPPool", CreateXPPrefab());

        // Setup weapon system
        weaponSystem.projectilePool = projectilePool.GetComponent<ObjectPool>();

        // Create Spawner
        var spawner = new GameObject("Spawner").AddComponent<Spawner>();
        spawner.enemyPool = enemyPool.GetComponent<ObjectPool>();
        spawner.player = player.transform;
        spawner.worldCamera = camera;
        spawner.spawnInterval = 1.0f;
        spawner.ringRadius = 14f;

        // Create Analytics
        var analytics = new GameObject("Analytics");
        analytics.AddComponent<StatsTracker>();
        analytics.AddComponent<SaveSystem>();
        var csvLogger = analytics.AddComponent<RuntimeCSVLogger>();
        csvLogger.intervalSeconds = 60f;
        csvLogger.logAtStart = true;

        // Create UI
        CreateUI(health, experience);

        Debug.Log("✅ SurvivorLite scene setup complete! Press Play to start!");
        EditorUtility.DisplayDialog("Setup Complete", 
            "SurvivorLite is ready to play!\n\n" +
            "Controls:\n" +
            "• WASD - Move\n" +
            "• ESC - Pause\n" +
            "• Auto-attack targets enemies\n\n" +
            "Press Play to start!", "Let's Go!");
    }

    static GameObject CreateSprite(string name, Vector3 position, Vector3 scale)
    {
        var go = new GameObject(name);
        go.transform.position = position;
        go.transform.localScale = scale;
        
        var renderer = go.AddComponent<SpriteRenderer>();
        // Use Unity's built-in white square sprite
        renderer.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
        if (renderer.sprite == null)
        {
            // Fallback: create a simple white texture
            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();
            renderer.sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), Vector2.one * 0.5f);
        }
        
        return go;
    }

    static GameObject CreatePool(string name, GameObject prefab)
    {
        var pool = new GameObject(name);
        var objectPool = pool.AddComponent<ObjectPool>();
        objectPool.prefab = prefab;
        objectPool.initialSize = 16;
        return pool;
    }

    static GameObject CreateProjectilePrefab()
    {
        var prefab = CreateSprite("pf_Projectile", Vector3.zero, new Vector3(0.3f, 0.3f, 1));
        prefab.tag = "Projectile";
        
        var renderer = prefab.GetComponent<SpriteRenderer>();
        renderer.color = Color.yellow;
        
        var collider = prefab.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = 0.1f;
        
        prefab.AddComponent<Projectile>();
        
        return prefab;
    }

    static GameObject CreateEnemyPrefab()
    {
        var prefab = CreateSprite("pf_Enemy", Vector3.zero, Vector3.one);
        prefab.tag = "Enemy";
        
        var renderer = prefab.GetComponent<SpriteRenderer>();
        renderer.color = Color.red;
        
        var rb = prefab.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        
        var collider = prefab.AddComponent<BoxCollider2D>();
        collider.size = Vector2.one * 0.8f;
        
        prefab.AddComponent<EnemyController>();
        var health = prefab.AddComponent<Health>();
        health.maxHP = 30;
        
        var dropper = prefab.AddComponent<DropOnDeath>();
        // Will be assigned after XP pool is created
        
        return prefab;
    }

    static GameObject CreateXPPrefab()
    {
        var prefab = CreateSprite("pf_XP", Vector3.zero, new Vector3(0.4f, 0.4f, 1));
        prefab.tag = "Pickup";
        
        var renderer = prefab.GetComponent<SpriteRenderer>();
        renderer.color = new Color(0, 1, 1); // Cyan
        
        var collider = prefab.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = 0.2f;
        
        var pickup = prefab.AddComponent<PickupXP>();
        pickup.xpValue = 5;
        pickup.magnetRange = 2f;
        
        return prefab;
    }

    static void CreateUI(Health playerHealth, Experience playerXP)
    {
        // Create Canvas
        var canvasGO = new GameObject("Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Create HP Bar
        var hpBarGO = new GameObject("HPBar");
        hpBarGO.transform.SetParent(canvasGO.transform);
        var hpSlider = hpBarGO.AddComponent<Slider>();
        hpSlider.minValue = 0;
        hpSlider.maxValue = 100;
        hpSlider.value = 100;
        
        // Position HP bar
        var hpRect = hpBarGO.GetComponent<RectTransform>();
        hpRect.anchorMin = new Vector2(0.02f, 0.9f);
        hpRect.anchorMax = new Vector2(0.3f, 0.95f);
        hpRect.offsetMin = Vector2.zero;
        hpRect.offsetMax = Vector2.zero;

        // Create XP Bar
        var xpBarGO = new GameObject("XPBar");
        xpBarGO.transform.SetParent(canvasGO.transform);
        var xpSlider = xpBarGO.AddComponent<Slider>();
        xpSlider.minValue = 0;
        xpSlider.maxValue = 20;
        xpSlider.value = 0;
        
        // Position XP bar
        var xpRect = xpBarGO.GetComponent<RectTransform>();
        xpRect.anchorMin = new Vector2(0.02f, 0.83f);
        xpRect.anchorMax = new Vector2(0.3f, 0.88f);
        xpRect.offsetMin = Vector2.zero;
        xpRect.offsetMax = Vector2.zero;

        // Create Timer Text
        var timerGO = new GameObject("Timer");
        timerGO.transform.SetParent(canvasGO.transform);
        var timerText = timerGO.AddComponent<Text>();
        timerText.text = "00:00";
        timerText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        timerText.fontSize = 24;
        timerText.color = Color.white;
        timerText.alignment = TextAnchor.MiddleCenter;
        
        // Position timer
        var timerRect = timerGO.GetComponent<RectTransform>();
        timerRect.anchorMin = new Vector2(0.45f, 0.9f);
        timerRect.anchorMax = new Vector2(0.55f, 0.95f);
        timerRect.offsetMin = Vector2.zero;
        timerRect.offsetMax = Vector2.zero;

        // Add UI controller
        var uiHud = canvasGO.AddComponent<UIHud>();
        uiHud.playerHealth = playerHealth;
        uiHud.playerXP = playerXP;
        uiHud.hpBar = hpSlider;
        uiHud.xpBar = xpSlider;
        // Note: timerText would need to be TMPro.TMP_Text for the script, 
        // but we'll leave it as regular Text for simplicity

        // Create pause menu
        CreatePauseMenu(canvasGO);
    }

    static void CreatePauseMenu(GameObject canvas)
    {
        // Create pause panel
        var pausePanel = new GameObject("PausePanel");
        pausePanel.transform.SetParent(canvas.transform);
        pausePanel.AddComponent<Image>().color = new Color(0, 0, 0, 0.8f);
        
        var pauseRect = pausePanel.GetComponent<RectTransform>();
        pauseRect.anchorMin = Vector2.zero;
        pauseRect.anchorMax = Vector2.one;
        pauseRect.offsetMin = Vector2.zero;
        pauseRect.offsetMax = Vector2.zero;
        
        pausePanel.SetActive(false);

        // Add pause menu component
        var pauseMenu = canvas.AddComponent<PauseMenu>();
        pauseMenu.pausePanel = pausePanel;

        // Create resume button
        var resumeBtn = CreateButton("Resume", pausePanel.transform, new Vector2(0, 50));
        resumeBtn.onClick.AddListener(() => pauseMenu.Resume());

        // Create quit button  
        var quitBtn = CreateButton("Quit", pausePanel.transform, new Vector2(0, -50));
        quitBtn.onClick.AddListener(() => pauseMenu.QuitToDesktop());
    }

    static Button CreateButton(string text, Transform parent, Vector2 position)
    {
        var buttonGO = new GameObject(text + "Button");
        buttonGO.transform.SetParent(parent);
        
        var button = buttonGO.AddComponent<Button>();
        buttonGO.AddComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
        
        var buttonRect = buttonGO.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(200, 50);
        buttonRect.anchoredPosition = position;

        // Add text
        var textGO = new GameObject("Text");
        textGO.transform.SetParent(buttonGO.transform);
        var textComp = textGO.AddComponent<Text>();
        textComp.text = text;
        textComp.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textComp.fontSize = 18;
        textComp.color = Color.white;
        textComp.alignment = TextAnchor.MiddleCenter;
        
        var textRect = textGO.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        return button;
    }
}
#endif