
# 📝 Conventional Commits Guide

This document explains the **Conventional Commits** standard, along with examples for your project.

---

## 📌 Basic Structure

```
<type>(<scope>): <subject>
```

- **`type`**: the type of change (required)
- **`scope`**: the area/module (optional but recommended)
- **`subject`**: short description (lowercase, no period)

---

## 🚦 Types and Examples

| Type       | Description                                         | Example commit message                               |
|------------|-----------------------------------------------------|------------------------------------------------------|
| **feat**     | Adding a new feature                                | `feat(auth): add Google login`                       |
| **fix**      | Fixing a bug                                        | `fix(api): handle empty response error`              |
| **docs**     | Updating documentation (README, API docs, comments)| `docs(readme): update usage instructions`            |
| **style**    | Code style changes (no logic changes)               | `style(ui): reformat CSS with Prettier`              |
| **refactor** | Code refactoring without feature or bug fix         | `refactor(order): extract billing logic to service`  |
| **perf**     | Improving performance                                | `perf(search): improve query performance with index` |
| **test**     | Adding or modifying tests                            | `test(auth): add unit test for login`                |
| **build**    | Changes to build system (webpack, vite, etc.)       | `build: update webpack config to support svg`        |
| **ci**       | Changes to CI/CD configuration                       | `ci(github): add action for automated testing`       |
| **chore**    | Minor chores not affecting the product               | `chore(log): add debug logs for troubleshooting`     |
| **revert**   | Reverting a previous commit                          | `revert: revert commit f123abc (fix auth issue)`     |

---

## ✨ Scope (`<scope>`) (Optional)

- Indicates the affected module/feature.
- Examples: `auth`, `api`, `invoice`, `build`, `metadata`.
- If the commit is broad, you can omit the scope.

---

## 🟢 Best Practices for Writing Subject

✅ Use **imperative** form (add, fix, update, remove, etc.)  
✅ **Do not capitalize** the first letter  
✅ **Do not end with a period**  

---

## 🚀 Full Examples

✅ Adding a new field:
```
feat(product): add discount field in product entity
```

✅ Fixing a bug:
```
fix(payment): handle missing payment method
```

✅ Improving performance:
```
perf(api): reduce response time by caching result
```

✅ Adding debug logs:
```
chore(log): add debug logs to track API requests
```

✅ Updating documentation:
```
docs(readme): update installation guide
```

✅ Refactoring code:
```
refactor(order): separate total calculation logic
```

✅ Adding tests:
```
test(invoice): add unit test for invoice generation
```

✅ Reverting a change:
```
revert: revert commit f123abc (fix bug in checkout flow)
```

---

## ⚡ Best Practices

- Group related changes into one commit.
- Link to issue/ticket if applicable:
  ```
  feat(auth): [JIRA-1234] add OTP login
  ```
- Use tools like:
  - **commitizen** – to generate commit messages.
  - **commitlint** – to enforce commit message format.
  - **husky** – to add Git hooks for checking commits.

---

## 🎯 Summary

✅ **Conventional Commits** help maintain a clear project history.  
✅ Make it easy to generate changelogs and automate CI/CD.  
✅ Promotes consistent communication in your team!

---

### 🔗 References

- [Conventional Commits](https://www.conventionalcommits.org/)
- [Commitizen](https://github.com/commitizen/cz-cli)
- [Commitlint](https://github.com/conventional-changelog/commitlint)
