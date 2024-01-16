using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Document
{
    [Key]
    public string DocumentID { get; set; }
    [MaxLength]
    public string DocumentName { get; set; }

    [MaxLength]
    public string BlobLocation { get; set; }

    [MaxLength] 
    public string Renderer { get; set; }

    public int Version { get; set; }
    // Other properties here
    }

public class DocumentParameter
{
    [Key]
    public Guid ID { get; set; }

    public Guid Parameter { get; set; }
    public string Document { get; set; }

    // Other properties here
    public virtual Document DocumentNavigation { get; set; }

    public virtual Parameter ParameterNavigation { get; set; }
}

public class Parameter
{
    [Key]
    public Guid ParameterID { get; set; }

    public string Name { get; set; }
    public Guid? Type { get; set; }
    public string Value { get; set; }

    // Other properties here

    public virtual ParameterType TypeNavigation { get; set; }
}

public class ParameterType
{
    [Key]
    public Guid ParameterTypeId { get; set; }

    public string Name { get; set; }

    // Other properties here
}
