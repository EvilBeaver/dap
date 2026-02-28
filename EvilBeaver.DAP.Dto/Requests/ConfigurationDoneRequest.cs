using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class ConfigurationDoneRequest : Request<ConfigurationDoneArguments>
{
    public ConfigurationDoneRequest() => Command = "configurationDone";
}

public class ConfigurationDoneArguments
{
}

public class ConfigurationDoneResponse : Response
{
}
