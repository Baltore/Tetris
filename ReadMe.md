# Tetris Game 🎮  

## 📖 Description

Ce projet est une implémentation classique du jeu Tetris, développée en C# avec WinForms. Il propose une expérience de jeu rétro fidèle à l'original, enrichie de fonctionnalités modernes telles que l'ajustement dynamique de la vitesse, le suivi du score et du temps, ainsi qu'une interface utilisateur personnalisée.

---

## ✨ Fonctionnalités

- 🎮 Gameplay classique : Déplacez, faites pivoter et verrouillez des formes pour compléter des lignes.
- ⚡ Gestion dynamique de la vitesse : Le jeu accélère automatiquement à mesure que le temps passe.
- ⏳ Suivi du score et du temps : Gardez une trace de vos performances en temps réel.
- 🛠️ Personnalisation de la vitesse : Configurez la vitesse initiale via le menu des options.
- 💻 Interface utilisateur simple : Conçu avec WinForms pour une expérience fluide et intuitive.

---

## 🗂️ Structure du projet

- TETRIS/
  - Game/
    - GameManager.cs       # Gestion principale du jeu
    - TimerManager.cs      # Gestion du temps et des seuils
    - ScoreManager.cs      # Gestion du score
    - Grid.cs              # Représentation de la grille de jeu
    - Shape.cs             # Modèle pour les formes de Tetris
    - ShapeFactory.cs      # Générateur de formes
    - PauseManager.cs      # Mettre en Pause le jeu
  - UI/
    - GameUI.cs            # Affichage de L'interface du jeu
    - MainMenu.cs          # Affichage du menu d'acceuil
    - OptionMenu.cs        # Affichage du menu option
- Program.cs               # Point d'entrée principal
- README.md                # Documentation du projet

---

## 🔧 Prérequis

- Système d'exploitation : Windows
- Langage : C# (Framework .NET 5.0 ou supérieur recommandé)
- IDE : Visual Studio ou Visual Studio Code avec le SDK .NET installé

---

## 🚀 Installation

1. Clonez le dépôt :
  ```bash
  git clone https://github.com/votre-nom/tetris-game.git
  cd tetris-game
  ```
2. Configurez le projet :
  Assurez-vous d'avoir installé le SDK .NET 5.0 ou une version supérieure.

3. Exécutez le projet :

  Ouvrez le projet dans Visual Studio ou VS Code.
  Compilez et exécutez l'application.

---

## 🕹️ Contrôles du jeu

- Flèche gauche : Déplacer la pièce à gauche
- Flèche droite : Déplacer la pièce à droite
- Flèche haut : Faire pivoter la pièce
- Flèche bas : Accélérer la descente

---

## 📊 Fonctionnalités avancées

- Gestion dynamique de la vitesse :
  La vitesse augmente automatiquement au fil du temps, rendant le jeu plus difficile.

- Menu des options :
  Configurez la vitesse initiale du jeu via un menu dédié.

- Grille et formes dynamiques :
  Le système prend en charge les formes standard de Tetris (I, O, T, L, Z, et leurs variantes inversées).

---

## 👤 Auteurs

- Qays
- Matthis

