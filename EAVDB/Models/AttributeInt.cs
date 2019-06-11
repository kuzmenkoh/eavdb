namespace EAVDB.Models
{
    public class AttributeInt<TEntity> : Attribute<TEntity>
    {
        public int IntValue { get; set; }
        public override object Value => IntValue;
    }
}
