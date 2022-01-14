using Domain.Interfaces;
using Domain.Models.StandardPlayingCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.BlackJack.Domain.Models
{
    internal class Player
    {
        internal IHand<StandardPlayingCard> Hand { get; }
    }
}
