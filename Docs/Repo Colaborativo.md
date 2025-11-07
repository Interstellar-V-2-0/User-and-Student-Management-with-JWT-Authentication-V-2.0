### 🧩 *1. Clonar el repositorio*

bash
git clone https://github.com/usuario/repositorio.git
cd repositorio


---

### 🌱 *2. Crear rama nueva desde develop (o main)*

bash
git fetch origin [-- este es para revisar que tu rama en la que estas parado y la rama de origin o remota esten iguales]
git checkout develop
git pull origin develop
git checkout -b feature/funcionalidad_nombre
# Ejemplo:
# git checkout -b feature/login_skibidi


---

### 💻 *3. Hacer cambios y subirlos*

bash
git add .
git commit -m "feat: agregar login por email (skibidi)"
git push -u origin feature/login_skibidi


---

### 🔄 *4. Traer cambios de main/develop a tu rama*

#### Opción A — Merge (más fácil)

bash
git fetch origin
git checkout feature/login_skibidi
git merge origin/develop
# Resolver conflictos si hay
git push


#### Opción B — Rebase (historial limpio)

bash
git fetch origin
git checkout feature/login_skibidi
git rebase origin/develop
# Resolver conflictos si hay
git push --force-with-lease


---

### 🚀 *5. Crear Pull Request en GitHub*

1. Ve a GitHub → pestaña *Pull requests* → *New pull request*
2. Base: develop o main
   Compare: feature/login_skibidi
3. Rellena título y descripción
4. Clic en *Create pull request*