using System.Collections.Generic;
using System.Linq;
using Models;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using Views;

namespace Controllers
{
    public class MapsController : CustomUI
    {
        [SerializeField] private MupVo[] mupId;
        
        private Dictionary<int, MapItemView> _mapItemViews;

        private void Start()
        {
            _mapItemViews = mupId.ToDictionary(id => id.MupId,
                itemView => itemView.MupItemView);

            foreach (var mapItem in _mapItemViews)
                if (mapItem.Key == 1)
                    mapItem.Value.EnableMap(true);
                else
                    mapItem.Value.EnableMap(false);
        }

        public void ActivateMap()
        {
            var varManager = Engine.GetService<ICustomVariableManager>();
            varManager.TryGetVariableValue<int>("MapID", out var intValue);
            foreach (var mapItem in _mapItemViews)
            {
                if (mapItem.Key == intValue)
                    mapItem.Value.EnableMap(true);
                else
                    mapItem.Value.EnableMap(false);
            }
        }

        public void ActiveLastMap()
        {
            foreach (var mapItem in _mapItemViews)
            {
                if (mapItem.Key != 2)
                    mapItem.Value.EnableLastMap(true);
                else
                    mapItem.Value.EnableLastMap(false);
            }
        }
    }
}