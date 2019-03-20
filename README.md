# 毕业设计

## 链接
* [论文及毕设相关材料](https://github.com/WhiskyHou/GraduationPaper)
* [服务端 RunnerMakerServer](https://github.com/WhiskyHou/RunnerMakerServer)

## 环境配置
* 需要在 Assets 目录下 import [龙骨动画Unity插件](https://github.com/DragonBones/DragonBonesCSharp/releases)
* 服务端项目地址 [RunnerMakerServer](https://github.com/WhiskyHou/RunnerMakerServer)

## 进度
### 3.4 - 3.8
* 本周工作
    1. 初步编写需求分析文档和游戏设计文档
    2. 基本完成界面原形设计和交互流程设计
    3. 构建 Unity 项目，进行了大致开发结构设计
    4. 探究部分技术点
        * 确定了 2D 开发模式，采用 Sprite 渲染
        * 确定使用 DragonBones 制作动画，学习其基本用法
        * 探究 BoxCollider2D 和 Rigidbody2D 的控制方法
* 下周工作
    1. 完成地图配置文件的规范设计
    2. 完成地图配置文件的读取更新
    3. 验证根据配置动态加载地图
    4. 继续完成游戏设计文档，完成组件的详细设计
    5. 完成已知需要的美术素材的收集

### 3.11 - 3.15
* 本周工作
    1. 初步完成根据配置文件动态加载地图功能
    2. 初步搭建服务端项目
        * 基于 NodeJs 进行开发
        * 地图信息以 json 格式进行文件储存
        * 用户信息考虑使用数据库储存
    3. 探究场景结构设计，决定采用 界面-场景 的模式
    4. 更新玩家角色控制器，调整控制手感，限制弹跳规则
    5. 新增了弹簧、大炮的预制体
    6. delay
        * 地图配置文件规范设计没完成
        * 游戏设计文档、组件设计没完成
        * 美术资源没收集完
* 下周工作
    1. 完成地图编辑模式的详细设计，初步实现客户端可进行编辑
    2. 完成服务端核心接口的设计，包括上传新地图、更新地图数据
    3. 探究用户登录和用户信息的实现
    4. 实现冰块、边界、激光的预制体
    5. 继续收集美术资源