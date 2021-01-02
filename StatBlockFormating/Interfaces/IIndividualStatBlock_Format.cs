using StatBlockCommon.Individual_SB;

namespace StatBlockFormating
{
    public interface IIndividualStatBlock_Format
    {
        string CreateFullText(IndividualStatBlock SB);
    }
}