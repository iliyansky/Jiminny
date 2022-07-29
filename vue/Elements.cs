using OpenQA.Selenium;

namespace vue
{
    public class Elements
    {
        public static By NewTaskTextField => By.ClassName("new-todo");
        public static By CheckAllTasksButton => By.CssSelector(".main > label");
        public static By ItemCounter => By.ClassName("todo-count");
        public static By Tab_All => By.LinkText("All");
        public static By Tab_Active => By.LinkText("Active");
        public static By Tab_Completed => By.LinkText("Completed");
        public static By ClearComplitedButton => By.ClassName("clear-completed");
    }
}
