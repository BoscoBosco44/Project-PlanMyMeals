The purpose of this project is to create a website that will give the user daily meal plans that fit their exact macro and calorie goals.

Meals are stored on the database and food nutrition information (per gram) is pulled from a food API. 

Everything will be done in grams and only grams because I hate every other unit of measurement when trying to cook/meal prep. 


Data needed per class:
Meal
User
Ingredient (obj saved in a meal)
Cals
Protein
Carbs
Fat
Ingredient list


Username
Password
Email
Cals goal
Protein goal
Carbs goal
Fat goal
Number of meals per day
Types of meals the user wants
isProtein
isFat
isCarbs




Plan for the meal ingredient adjustment algo: (example with 4 meals per day)
Pick 4 random meals from database with cals and macros close to the users goals (range per meal: protein +- 50g, carbs: +- 100g, fat: +- 20g, cals: +- 300 cals) (the kind of meal, breakfast, lunch, dinner etc, will be determined by the user in user settings)
Get nutrition info for each ingredient per one gram (1g of x ingredient = Xg carbs, Xg protein, Xg fat, X num of calories)
Find the ratio of calories between meals (if the 4 meals have cal values of 350, 450, 500, and 700 the ratio would be 3.5 : 4.5 : 5 : 7)
Scale the calorie total up or down to meet the users goals while staying consistent with the ratios found in the previous step (this will be done by adjusting all ingredient amounts up or down until the calories fit the ratios)


