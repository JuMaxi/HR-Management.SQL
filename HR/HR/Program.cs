using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using Spectre.Console;

namespace HR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SelectionKindOfManagement SelectionMenu = new SelectionKindOfManagement();

            SelectionMenu.ChooseKindMenu();
        }
    }
}
