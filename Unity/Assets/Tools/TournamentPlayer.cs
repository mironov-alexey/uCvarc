using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;

namespace Assets
{
    public class TournamentPlayer
    {
        public readonly ConfigurationProposal configProposal;
        public readonly IWorldState preferredWorldState;
        public readonly IMessagingClient client;

        public TournamentPlayer(IMessagingClient client, ConfigurationProposal configProposal,
            IWorldState preferredWorldState)
        {
            this.client = client;
            this.configProposal = configProposal;
            this.preferredWorldState = preferredWorldState;
        }
    }
}
