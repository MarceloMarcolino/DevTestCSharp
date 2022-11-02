# Turn RPG system - DevLog
 Kokku Software Engineer Applicant Test
 
 The Extra Feature for this project is adding a unique feature to each class, since I was born in April.
 
 This is an RPG-sh game that automatically runs a battle fight between two characters of different types.
 
 ###Logs
 1. The folder structure was modified, some extra files that were duplicated, such as Character.cs and Grid.cs, were deleted;
 2. The project core is located at the Program.cs file. It's going to run the game main thread and the game logic;
 3. The HUD class is responsible for dealing with the output communication Human Computer Interaction (HCI);
 4. The UserInput class is responsible for dealing with the input communication Human Computer Interaction (HCI);
 5. The Grid class is responsible for creating and managing the Grid positioning and rendering;
 6. The Character class is responsible for handling concrete implementation of the character's multiple states throughout the game;
 7. The Team class is responsible for managing the Teams' states;
 8. The Utils class gives a toolset of functions that can be accessed by multiple other classes interchangeably;
 9. The Types file unifies many different structs and classes that are used as data structures for the game logic;
 10. The CharacterFactory class is responsible for creating instances of characters, pulled from the class of the same name.