using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TierlistServer.Application.DTOs.Games;

public record AddGameDto(
    int RawgId,
    string Title,
    string BackgroundImage,
    int TierListId
);
