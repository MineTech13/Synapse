﻿
using Harmony;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Synapse.Api
{
    public class Map
    {
        internal Map() { }

        public static Map Get => Server.Get.Map;

        public List<Tesla> Teslas { get; } = new List<Tesla>();

        public List<Elevator> Elevators { get; } = new List<Elevator>();

        public List<Door> Doors { get; } = new List<Door>();

        public List<Room> Rooms { get; } = new List<Room>();

        public Dummy CreateDummy(Vector3 pos, Quaternion rot, RoleType role = RoleType.ClassD, string name = "(null)", string badgetext = "", string badgecolor = "") 
            => new Dummy(pos, rot, role, name, badgetext, badgecolor);

        internal void RefreshObjects()
        {
            SynapseController.Server.Logger.Info("Refresh Objects");

            Teslas.Clear();
            foreach (var tesla in SynapseController.Server.GetObjectsOf<TeslaGate>())
                SynapseController.Server.Map.Teslas.Add(new Tesla(tesla));

            Rooms.Clear();
            foreach (var room in SynapseController.Server.GetObjectsOf<Transform>().Where(x => x.CompareTag("Room") || x.name == "Root_*&*Outside Cams" || x.name == "PocketWorld"))
                Rooms.Add(new Room(room.gameObject));
        }
    }
}
