using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Services
{
    public class DoorsService : MonoBehaviour
    {
        [SerializeField] private List<DoorData> _doors = new List<DoorData>();

        public List<DoorData> Doors => _doors;

        [ContextMenu(nameof(CollectDoors))]
        private void CollectDoors()
        {
            _doors = FindObjectsOfType<DoorData>().ToList();
        }
    }
}
