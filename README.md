# Cat_Coffee

一款 2D 猫咖经营建造原型（Unity 2022.3 / URP 2D）。

在网格地块上摆放多格占地的设施建筑，配合完整的日历时间系统（时 / 日 / 月 / 年 / 四季），搭建猫咖经营游戏的基础框架。目前为原型阶段：建造与时间两大核心系统已落地，顾客、订单与经济循环尚未完成。

## 核心系统

### 网格建造（`Assets/Scripts/Grid`、`Assets/ScriptObject`）

- **泛型网格** `Grid<T>`：按宽高与格长生成地块网格与每格的地面 Mesh，提供世界坐标 / 格坐标换算
- **多格建筑**：建筑数据用 ScriptableObject（`Builds`）配置 — 精灵图、占格尺寸（`Vector2Int Size`）、偏移，建造时按尺寸占位
- **建造交互**：建造按钮、设施悬浮提示（ToolTip）、自定义光标管理（`Scripts/Cusor`）

### 时间系统（`Assets/Scripts/Time`）

- 游戏内时钟：秒 / 分 / 时 / 日 / 月 / 年完整推进，每 3 个月为一季
- 基于事件广播时间变化（`EventHandler`），UI 时钟（`TimeUI`）订阅显示
- 预留光照班次（LightShift）事件接口，供后续昼夜光照表现接入

### UI 框架（`Assets/Scripts/UI`）

- `BasePanel` + `UIManager` 面板式管理，面板与按钮动效基于 DOTween
- 通用组件：单例基类、事件处理器、全局设置（`Scripts/Utilities`）

## 技术栈

| 项 | 内容 |
|---|---|
| 引擎 | Unity 2022.3.53f1c1 |
| 渲染 | URP 14.0.11（2D） |
| 动效 | DOTween Pro（第三方插件） |
| 工具库 | CodeMonkey Utilities（第三方） |
| 场景 | `Assets/Scenes/Main.unity`（主场景）、`UI.unity` |
| 自研代码 | `Assets/Scripts` + `Assets/ScriptObject` 约 1,000 行 |

## 项目结构

```text
Assets/
├── Scenes/
│   ├── Main.unity        # 主场景
│   └── UI.unity          # UI 场景
├── Scripts/
│   ├── Grid/             # 泛型网格、网格管理、建造预设
│   ├── Time/             # 时间系统逻辑与时钟 UI
│   ├── UI/               # 面板框架、动效、设施提示
│   ├── Cusor/            # 自定义光标
│   └── Utilities/        # 单例、事件、枚举、设置
├── ScriptObject/
│   └── Builds.cs         # 建筑数据（ScriptableObject）
├── CodeMonkey/           # 第三方：CodeMonkey 工具库
└── Plugins/Demigiant/    # 第三方：DOTween / DOTween Pro
```

## 运行方式

1. 用 Unity **2022.3.53f1** 打开本项目；
2. 打开 `Assets/Scenes/Main.unity` 并运行。

## 项目状态

原型项目（2025 年 2–3 月）。已完成：网格建造、多格建筑配置、时间/季节系统、UI 框架。未完成：顾客 AI、订单与经济循环、存档。工程中已引入 Unity Ads 与 IAP 包，但未接入实际变现流程。

## 第三方说明

- **DOTween Pro**（`Assets/Plugins/Demigiant/`）与 **CodeMonkey Utilities**（`Assets/CodeMonkey/`）为第三方组件，版权归各自作者所有；
- 本仓库的游戏代码为原创。
