# FPS-шутер прототип (стиль CS2)

Этот репозиторий содержит заготовку сценариев Unity для одиночного прототипа: передвижение игрока, базовая стрельба, ИИ врагов, установка бомбы на точке и базовые подсказки по структуре ассетов.

## Структура `Assets`
- `Scripts/Player/` — управление персонажем и оружием (`PlayerController`, `PlayerLook`, `WeaponController`).
- `Scripts/Combat/` — общее здоровье и простейший AI преследования (`Health`, `EnemyAI`).
- `Scripts/Objective/` — зона установки бомбы и логика бомбы (`BombSite`, `BombController`).
- `Scripts/Audio/` — сюда можно добавить менеджер звука (задайте при необходимости).
- `Prefabs/`, `Models/`, `Materials/`, `Audio/` — заготовленные папки для ваших ассетов.

## Подключение скриптов
1. **Игрок**: Prefab с `CharacterController`, `PlayerController`, `PlayerLook`, `WeaponController`, `Health`, камерой (привяжите в `cam`), `AudioSource`, `ParticleSystem` для дульного огня.
2. **Оружие**: Задайте звуки выстрела/перезарядки, `magSize`, `fireRate`; подключите `muzzleFlash`. Стрельба использует `Physics.Raycast`.
3. **Враги**: Prefab с `NavMeshAgent`, `EnemyAI`, `Health`, коллайдером; на сцене должен быть NavMesh.
4. **Бомба и сайт**: Добавьте на сцену `BombSite` и укажите его в `BombController` перед установкой. Метод `TryPlant` вызывайте из контроллера игрока при удержании кнопки установки.

## Пример интеграции бомбы с игроком
```csharp
public BombController bombPrefab;
public BombSite bombSite;
private BombController carriedBomb;

void Start()
{
    carriedBomb = Instantiate(bombPrefab);
    carriedBomb.gameObject.SetActive(false);
}

void Update()
{
    if (Input.GetKey(KeyCode.G) && carriedBomb != null)
    {
        carriedBomb.targetSite = bombSite;
        carriedBomb.transform.position = transform.position + transform.forward * 0.5f;
        carriedBomb.gameObject.SetActive(true);
        StartCoroutine(carriedBomb.TryPlant(transform));
        carriedBomb = null;
    }
}
```

## Настройка сцены
1. Добавьте уровень/terrain и запеките NavMesh.
2. Отметьте точку установки бомбы объектом с `BombSite` (радиус отображается Gizmo).
3. Настройте Input: `Horizontal`, `Vertical`, `Jump`, `Fire1`, `R` для перезарядки, `G` для установки бомбы.
4. Загрузите собственные модели, текстуры и звуки в соответствующие папки, затем привяжите их к префабам.

Эти скрипты дают основу: ходьба, стрельба, ИИ врагов и установка/взрыв бомбы. Дальше можно расширять визуальные эффекты, UI и сетевой функционал.

## Сборка `.exe`
В репозитории есть `Assets/Editor/BuildScript.cs` с методом `BuildScript.BuildWindows`, который собирает Windows 64-бит билд в `Build/Windows/FPSPrototype.exe`.

### Через Unity Editor
1. Откройте проект в Unity с установленным модулем **Windows Build Support (IL2CPP)**.
2. File → Build Settings → PC, Mac & Linux Standalone → выберите **Target Platform: Windows** и нажмите **Build** (или **Build and Run**), указав папку `Build/Windows`.

### Через CLI (автоматизация/CI)
```
unity -quit -batchmode -nographics \
  -projectPath /путь/до/репозитория \
  -executeMethod BuildScript.BuildWindows
```

Если Unity не установлена в среде, `.exe` не будет создан. Соберите билд локально или на CI с установленным Unity Editor.
