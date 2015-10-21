using Assets;
using CVARC.V2;
using TheBeachBots;


namespace TBBCompetitions
{
    public class Level1 : Competitions
    {
        public Level1()
            : base(new TBBLogicPartHelper(), new UnityEnginePack(), new TBBManagerPart())
        { }
    }
}
