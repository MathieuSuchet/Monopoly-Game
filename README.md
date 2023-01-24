# Monopoly-Game
 
This repo is a recreation of the world-known game "Monopoly" on PC using Unity. I personally enjoy playing this game and therefore decided to make my own with unique ideas

If you want to contribute to the game creation, you can fill this form which tackles game ideas or just feedbacks on the classic monopoly game, it is anonymous : 
https://docs.google.com/forms/d/e/1FAIpQLSdkAoGbtukcT7r-xQwgHifRqsMC7DZtTcy2XQI0ouQ27kI5bw/viewform?usp=sf_link

## Actual advancements of the project

### Basic monopoly features :
- [x] Rent
- [x] Buying
- [x] Selling
- [ ] Mortgage (WIP)
- [x] Bankruptcy
- [x] Moving on the board

### My ideas :
- [x] Random mode (Shuffle the board, prices remain)
- [ ] Chaos mode (WIP) (More money to start with, multipliers on rents, etc.)
- [ ] Minigames mode (Similar to Mario party kind of games)

### Unity (2D) :
- [ ] Creation of the board
- [ ] Creation of the player (along with movement, dice throw, choices, etc.)
- [ ] Handling the camera play
- [ ] UI
- [ ] Main menu

### Unity (3D):
- [ ] Creation of the board
- [ ] Creation of the player (along with movement, dice throw, choices, etc.)

1st release objective :
- Functional Monopoly game
- Random mode
- Unity board, player and UI functional (2D)

## For developers interested in the project

I'm not working with anyone, this project is mainly here to help me progress as a developer, i'm not willing to take it as a serious game right now. I might in the future, but for now, i don't.

This project is separated in 5 parts :

### Monopoly Tests
This project covers the whole unit tests, i always keep a keen eye on that project given the fact that the algorithm running behind the scenes can break quite easily

### Monopoly Lib
This project is the backbone of the game, it is a library that i created to use for the graphical part (Unity), i'm not going to go into the details of how the alogrithm work, it is a "long" algorithm rather than a "complicated" one.

### Monopoly Console
A quick project quite similar to the test one, except that here, i test features in order to enhance them or in order to judge their usefulness.

### Monopoly Simulator
A project using WPF, it is an interface made to trigger the monopoly game and have details displayed on a much more pleasant UI (for me), it is kind of the same as the console project, yet it's heavier. This project sends data to MonopolyStats.

### Monopoly Stats
A python project created in order to balance the game. In this project, i display stats and i decide on what to change based on the user preference (some user prefer short games, this project is what drives it)
