using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    class Recipe
    {
        public int Num { get; set; }
        public string Ingredients { get; set; }
        public List<string> IngList { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int numIngredients
        {
            get
            {
                return IngList.Count();
            }
        }

        public void RandomizeIngredients()
        {
            // To give the items different heights for visual variety, give recipes
            // random numbers of random "extra" ingredients
            Random rndNum = new Random();
            Random rndIng = new Random();

            ObservableCollection<string> extras = new ObservableCollection<string>{
                                                        "Garlic",
                                                        "Lemon",
                                                        "Butter",
                                                        "Lime",
                                                        "Feta Cheese",
                                                        "Parmesan Cheese",
                                                        "Breadcrumbs"};
            for (int i = 0; i < rndNum.Next(0, 4); i++)
            {
                string newIng = extras[rndIng.Next(0, 6)];
                // If the ingredient is not already present in the recipe, add it
                if (!IngList.Contains(newIng))
                {
                    Ingredients += "\n" + newIng;
                    IngList.Add(newIng);
                }
            }

        }
    }
}
