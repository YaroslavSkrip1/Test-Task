using Controllers;
using Naninovel;

namespace Command
{
    [CommandAlias("enableMap")]
    public class EnableMapCommand : Naninovel.Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var uiManager = Engine.GetService<UIManager>();
            var questScreen = uiManager.GetUI<MapsController>();
            questScreen.ActiveLastMap();
            return UniTask.CompletedTask;
        }
    }
}