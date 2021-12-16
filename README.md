<h1 align="center">afdian-action</h1>

> 🔧 爱发电 自动生成赞助页面 | GitHub Action

[![repo size](https://img.shields.io/github/repo-size/yiyungent/afdian-action.svg?style=flat)]()
[![LICENSE](https://img.shields.io/github/license/yiyungent/afdian-action.svg?style=flat)](https://github.com/yiyungent/afdian-action/blob/main/LICENSE)
[![QQ Group](https://img.shields.io/badge/QQ%20Group-894031109-deepgreen)](https://jq.qq.com/?_wv=1027&k=q5R82fYN)


## 介绍

使用 `GitHub Action` 自动生成 爱发电 赞助页面, 无需再手动更新赞助列表。

> 效果见 [Sponsor.md](https://github.com/yiyungent/afdian-action/blob/main/Sponsor.md)

## 功能

- [x] 自动生成 爱发电 赞助页面
- [x] 支持 Razor 语法, 高度灵活 (编程式自定义模板), 可使用模板文件自定义样式风格

## 使用

### 1. 创建 模板文件 afdian-action.cshtml

> .github/afdian-action.cshtml

> 此模板 可用 Razor 语法, 可自定义

```cshtml
@{
    var viewModel = (Afdian.Action.ViewModels.AfdianViewModel)@Model;
}

@for (int i = 0; i < viewModel.Sponsor.data.list.Count(); i++)
{
    @{ 
        var sponsorItem  = viewModel.Sponsor.data.list[i];
     }

     <a href="https://afdian.net/u/@sponsorItem.user.user_id">
         <img src="@sponsorItem.user.avatar?imageView2/1/w/120/h/120" width="40" height="40" alt="@sponsorItem.user.name" title="@sponsorItem.user.name"/>
     </a>

}
```

> 补充: 此模板 风格来自 <https://github.com/CnGal/CnGalWebSite>

### 2. 修改 目标文件: 如: README.md

> 添加如下 `开始,结束标志`, 此标志中间将插入模板解析后的赞助列表

```markdown
## 赞助者

感谢这些来自爱发电的赞助者：

<!-- AFDIAN-ACTION:START -->
<!-- AFDIAN-ACTION:END -->
```

### 2. 创建 afdian-action.yml

> .github/workflows/afdian-action.yml

```yml
name: afdian-action

on:
  schedule: # Run workflow automatically
    - cron: '0 * * * *' # Runs every hour, on the hour
  workflow_dispatch: # Run workflow manually (without waiting for the cron to be called), through the Github Actions Workflow page directly

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout source
        uses: actions/checkout@v2
        with:
          ref: main # 注意修改为你的分支, 例如: master

      - name: Afdian action
        uses: yiyungent/afdian-action@main
        with:
          # 在 Settings->Secrets 配置 AFDIAN_USERID, AFDIAN_TOKEN
          # 爱发电 user_id
          afdian_userId: ${{ secrets.AFDIAN_USERID }}
          # 爱发电 token
          afdian_token: ${{ secrets.AFDIAN_TOKEN }}
          # 默认为: .github/afdian-action.cshtml
          template_filePath: ".github/afdian-action.cshtml"
          # 默认为: README.md
          target_filePath: "README.md"

      # 下方为 直接 push 到目标分支, 当然你也可以选择 Pull Request 方式
      - name: Commit files
        run: |
          git config --local user.email "41898282+github-actions[bot]@users.noreply.github.com"
          git config --local user.name "github-actions[bot]"
          git commit -m "Add changes: 爱发电赞助" -a
      
      - name: Push changes
        uses: ad-m/github-push-action@master # https://github.com/ad-m/github-push-action
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          branch: main # 注意修改为你的分支, 例如: master

```

> 如果你不想 直接 push, 那么也可以通过 `Pull Request`

```yml
    # 发起 Pull Request
    # Make changes to pull request here
    # https://github.com/peter-evans/create-pull-request
    - name: Create Pull Request
      uses: peter-evans/create-pull-request@v3
      with:
        commit-message: "Update 爱发电 赞助"
        branch: afdian-action/update
        delete-branch: true
        branch-suffix: "short-commit-hash"
        title: '[afdian-action] Update 赞助'
        body: "更新 爱发电 赞助"
        labels: |
          afdian-action
          automated pr
```

> 生成效果见 [Sponsor.md](https://github.com/yiyungent/afdian-action/blob/main/Sponsor.md)

## Related Projects

- [yiyungent/coo: 🧰 .NET 自用CLI, 工具集](https://github.com/yiyungent/coo)    
  - 本项目 核心依赖


## Donate

afdian-action is an MIT licensed open source project and completely free to use. However, the amount of effort needed to maintain and develop new features for the project is not sustainable without proper financial backing.

We accept donations through these channels:
- <a href="https://afdian.net/@yiyun" target="_blank">爱发电</a>

## Author

**afdian-action** © [yiyun](https://github.com/yiyungent), Released under the [MIT](./LICENSE) License.<br>
Authored and maintained by yiyun with help from contributors ([list](https://github.com/yiyungent/afdian-action/contributors)).

> GitHub [@yiyungent](https://github.com/yiyungent) Gitee [@yiyungent](https://gitee.com/yiyungent)


