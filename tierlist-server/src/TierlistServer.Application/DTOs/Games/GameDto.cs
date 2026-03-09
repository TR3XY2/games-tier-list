using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TierlistServer.Application.DTOs.Games;

public record GameDto(
    int Id,
    int RawgId,
    string Title,
    string BackgroundImage,
    string? Tier
);
