using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using Color = System.Drawing.Color;


namespace ConsoleApplication2
{
    class Program
    {

        private static AIHeroClient hero 
        {
            get { return ObjectManager.Player; }
        }
        
        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Targeted E;
        public static Menu RyzeMenu, Drawings, Combo;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += GameLoad;
            Drawing.OnDraw += OnDraw;
        }

        //Função que é ativada assim que o jogo carrega
        private static void GameLoad(EventArgs args)
        {
            //Verifica o nome do champion e se for igual a ryze corre o resto
            if (hero.ChampionName != "Ryze") return;

            //Criar a função que é executada durante o jogo
            Chat.Print("Evolved Ryze Loaded");
            Game.OnTick += OnTick;

            //Menu principal
            RyzeMenu = MainMenu.AddMenu("Ryze", "Main");

            //Cria um texto dentro do menu principal
            RyzeMenu.AddLabel("This was made by Asserio");

            //Sub menu de desenho
            Drawings = RyzeMenu.AddSubMenu("Drawings", "Draw");
            Drawings.AddGroupLabel("Range");
            Drawings.Add("Q Range", new CheckBox("Draw Q", true));
            Drawings.Add("W Range", new CheckBox("Draw W", true));

            //Combo
            Combo = RyzeMenu.AddSubMenu("ComboMode", "Combo");

            //Skillshots
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 1500, 100);
            W = new Spell.Targeted(SpellSlot.W, (uint)hero.Spellbook.GetSpell(SpellSlot.W).SData.CastRangeDisplayOverride);
        }
        //funcao OnTick
        private static void OnTick(EventArgs args)
        {

        }
        //funcao OnDraw
        private static void OnDraw(EventArgs args)
        {
            if (hero.IsDead == false)
            {
                if (Drawings["Q Range"].Cast<CheckBox>().CurrentValue)
                {
                    new Circle() { Color = Color.Red, Radius = Q.Range, BorderWidth = 2f }.Draw(hero.Position);

                }
                if (Drawings["W Range"].Cast<CheckBox>().CurrentValue)
                {
                    new Circle() { Color = Color.White, Radius = W.Range, BorderWidth = 2f }.Draw(hero.Position);
                }
            }
        }
    }
}