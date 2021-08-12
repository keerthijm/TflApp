using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TflApp
{
    public static class JsonTokenReaderUtility
    {
        public static Road ConvertToObject(JToken token)
        {
            Road road;
            switch (token.Type)
            {
                case JTokenType.Array:
                    road = token.ToObject<List<ValidRoad>>()[0];
                    break;

                case JTokenType.Object:
                    road = token.ToObject<InvalidRoad>();
                    break;
                default:
                    throw new NotImplementedException(token.Type + " is not defined");
            }
            return road;
        }
    }
}
