using Controllers;
using Naninovel;

namespace Command
{
    [CommandAlias("DecreaseMapLevel")]
    public class DecreaseMapLevelCommand : Naninovel.Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var varManager = Engine.GetService<ICustomVariableManager>();
            varManager.TryGetVariableValue<int>("MapID", out var intValue);
            intValue -=1;
            varManager.TrySetVariableValue("MapID", intValue);
            var uiManager = Engine.GetService<UIManager>();
            var questScreen = uiManager.GetUI<MapsController>();
            questScreen.ActivateMap();
            return UniTask.CompletedTask;
        }
    }
}