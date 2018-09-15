===================

Client server application of a Belote card game.

----------


Launching the game
-------------

- Launch the client application. Double click on clientServerApp/clientApp/bin/Debug/clientApp.exe
- Enter the IP Address displayed in the server window.

Testing the game
-------------

- start by launching the server Application. Double click on clientServerApp/serverApp/bin/Debug/serverApp.exe
- launch 4 clients (view the 'Launching the game' part to see how to achieve this)

Game rules
----------------
Belote is a trick taking game: each of the four players plays one card in turn, and the one who played the highest of the 4 cards wins the trick. Then, he collects the cards played, and leads to the next trick. On a trick, there are several simple rules to follow:

- The player who leads to a trick can play any card he wants (of any suit, of any rank). 
-  The suit of this first card fixes what is called the suit led.
- Then, the 3 next players must imperatively play a card of the suit led (if they have at least one).
- If a player has no more cards in the suit led, he discards, that is, he plays any card he wants.
- When the 4 players have played, the one who played the highest card of the suit led wins the trick. The cards of the other suits cannot win the trick. The player who won the trick collects the cards and starts again.

Endpoints
--------------
In order to communicate are available 8 different enpoints, each having a particular role : 
- *Greetings* : to give an id and to attribute room
- *WhichTasks* : to tell which task he has to do
- *GetHands*: to distribute the hand
- *GetTrumps* : to give the trump
- *GetBoards* : to show the board
- *Responses* : answer to the question 'do you want the trump'
- *PutCards* : to play a card
- *GetScores* : to display the scores
