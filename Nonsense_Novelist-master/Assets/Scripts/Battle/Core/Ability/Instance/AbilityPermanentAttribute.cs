
using System.Collections.Generic;

public class AbilityPermanentAttribute : AbilityBase
{
    private Formula mAttributeType = new Formula("attr_type");
    private Formula mPercent = new Formula("attr_percent");
    private Formula mValue = new Formula("attr_value");

    public override void AddParams()
    {
        mParams.Add(mAttributeType);
        mParams.Add(mPercent);
        mParams.Add(mValue);
    }

    protected override void OnInit()
    {
        Unit.ModifyBase((AttributeType)mAttributeType.EvaluateInt(this), mValue.Evaluate(this), mValue.EvaluateBool(this));
    }
}