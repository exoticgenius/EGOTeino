using Fractal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EGOTeino.Framework.Core
{
    public class GearBox : Node
    {
        public static int FEATURES_LIMIT = 0;

        /// <summary>
        /// clreate an empty gearbox
        /// </summary>
        public GearBox() : base("GearBox")
        {
            SetTypeName(this.GetType());
            for (int i = Features.Count; i < FEATURES_LIMIT; i++) Features.Add("");
            EmitFinish += GearBox_EmitFinish;
        }

        /// <summary>
        /// remove all non gear items to prevent casing errors
        /// </summary>
        /// <param name="sender"></param>
        private void GearBox_EmitFinish(INode sender)
        {
            RemoveChild(x => !(x is Gear));
        }

        /// <summary>
        /// get all available gears
        /// </summary>
        /// <returns></returns>
        public List<Gear> GetGears()
        {
            return PullChildren<Gear>();
        }

        /// <summary>
        /// get gear by name
        /// </summary>
        /// <param name="name">gear name</param>
        /// <returns></returns>
        public Gear GetGear(string name)
        {
            return PullChildren<Gear>().Find(x => x.Name == name);
        }

        /// <summary>
        /// add gear to gearbox list
        /// </summary>
        /// <param name="gear">gear to add</param>
        /// <returns>returns false when name conflict exist</returns>
        public bool AddGear(Gear gear)
        {
            if (PullChildren<Gear>().FindAll(x => x.Name == gear.Name).Count > 0) return false;
            AddChild(gear);
            return true;
        }
        /// <summary>
        /// add gear to gearbox list
        /// </summary>
        /// <param name="gear">gear to add</param>
        /// <returns>returns false when name conflict exist</returns>
        public bool AddGear(string name, string value)
        {
            return AddGear(new Gear(name, value));
        }
    }
}
