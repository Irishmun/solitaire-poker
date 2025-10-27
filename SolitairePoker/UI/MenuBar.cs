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
            TSMI_ChooseDeck = new ToolStripMenuItem("Decks");
            TSMB_ToggleMute = new ToolStripButton("Mute Audio");
            ToolStripSeparator sep = new ToolStripSeparator();
            TSMB_ToggleMute.CheckOnClick = true;
            TSMB_ToggleMute.Width = 64;

            fileStrip.DropDownItems.Add(TSMB_ToggleMute);
            fileStrip.DropDownItems.Add(sep);
            fileStrip.DropDownItems.Add(TSMI_CloseGame);
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
        public ToolStripButton TSMB_ToggleMute { get; set; }
    }
}
