using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolitairePoker.UI
{
    public class MenuBar
    {
        private Form _form;
        public MenuBar(Form form)
        {
            this._form = form;
        }

        public void Initialize()
        {
            MenuStrip strip = new MenuStrip();
            ToolStripMenuItem fileStrip = new ToolStripMenuItem("File");
            TSMI_CloseGame = new ToolStripMenuItem("Exit.");
            fileStrip.DropDownItems.Add(TSMI_CloseGame);
            TSMI_ChooseDeck = new ToolStripMenuItem("Decks");

            strip.Items.Add(fileStrip);
            strip.Items.Add(TSMI_ChooseDeck);
            _form.MainMenuStrip = strip;
            _form.Controls.Add(strip);
        }

        public void AddDecksToDropDown(string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                TSMI_ChooseDeck.DropDownItems.Add(names[i]);
            }
        }

        public ToolStripMenuItem TSMI_CloseGame { get; set; }
        public ToolStripMenuItem TSMI_ChooseDeck { get; set; }
    }
}
