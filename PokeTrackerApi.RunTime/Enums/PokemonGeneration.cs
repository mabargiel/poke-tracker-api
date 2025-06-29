using System.Runtime.Serialization;

namespace PokeTrackerApi.RunTime.Enums;

public enum PokemonGeneration
{
    [EnumMember(Value = "Generation I")] GenerationI,

    [EnumMember(Value = "Generation II")] GenerationII,

    [EnumMember(Value = "Generation III")] GenerationIII,

    [EnumMember(Value = "Generation IV")] GenerationIV,

    [EnumMember(Value = "Generation V")] GenerationV,

    [EnumMember(Value = "Generation VI")] GenerationVI,

    [EnumMember(Value = "Generation VII")] GenerationVII,

    [EnumMember(Value = "Generation VIII")]
    GenerationVIII,

    [EnumMember(Value = "Generation IX")] GenerationIX
}
