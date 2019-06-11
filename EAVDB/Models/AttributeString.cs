namespace EAVDB.Models
{
    public class AttributeString<TEntity> : Attribute<TEntity>
    {
        public string StringValue { get; set; }

        public override object Value => StringValue;
    }
}
