using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Data.Services.ExceptionHandler
{
    public class ResponseException
    {
        private Exception _exception;

        public ResponseException(HttpResponseMessage response)
        {
            try
            {
                var httpErrorObject = response.Content.ReadAsStringAsync().Result;

                // Create an anonymous object to use as the template for deserialization:
                var anonymousErrorObject =
                    new { message = "", ModelState = new Dictionary<string, string[]>() };

                // Deserialize:
                var deserializedErrorObject =
                    JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

                // Now wrap into an exception which best fullfills the needs of your application:
                var ex = new Exception();

                // Sometimes, there may be Model Errors:
                if (deserializedErrorObject != null && deserializedErrorObject.ModelState != null)
                {
                    var errors =
                        deserializedErrorObject.ModelState
                                                .Select(kvp => string.Join(". ", kvp.Value));
                    for (int i = 0; i < errors.Count(); i++)
                    {
                        // Wrap the errors up into the base Exception.Data Dictionary:
                        ex.Data.Add(i, errors.ElementAt(i));
                    }
                }
                // Othertimes, there may not be Model Errors:
                else
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            {
                                var error = JsonConvert.DeserializeObject<ResponseUnauthorized>(httpErrorObject);
                                ex = new Exception("Unauthorized");
                            }
                            break;
                        case HttpStatusCode.Forbidden:
                            {
                                ex = new Exception("Forbidden");
                            }
                            break;
                        case HttpStatusCode.BadRequest:
                            {
                                ex = new Exception("Bad Request");
                                var error = JsonConvert.DeserializeObject<ResponseBadRequest>(httpErrorObject);
                                if (error.IsModelValidatonError)
                                {
                                    foreach (var err in error.Errors)
                                    {
                                        ex.Data.Add(err.Name, err.Reason);
                                    }
                                }
                            }
                            break;
                        case HttpStatusCode.UnprocessableEntity:
                            {
                                ex = new Exception("Unprocessable Entity");
                                var error = JsonConvert.DeserializeObject<ResponseUnprocessableEntity>(httpErrorObject);
                                if (error.ValidationErrors != null && error.ValidationErrors.Count() > 0)
                                {
                                    foreach (var err in error.ValidationErrors)
                                    {
                                        ex.Data.Add(err.Name, err.Reason);
                                    }
                                }
                            }
                            break;
                        case HttpStatusCode.InternalServerError:
                            {
                                ex = new Exception("Internal Server Error");
                                var error = JsonConvert.DeserializeObject<ResponseUnauthorized>(httpErrorObject);
                                ex.Data.Add("Title", error.Title);
                                ex.Data.Add("Detail", error.Detail);
                                ex.Data.Add("Status", error.Status);
                            }
                            break;
                        case HttpStatusCode.NotFound:
                            {
                                ex = new Exception("Not Found");
                                var error = JsonConvert.DeserializeObject<ResponseUnauthorized>(httpErrorObject);
                                ex.Data.Add("Title", error.Title);
                                ex.Data.Add("Detail", error.Detail);
                                ex.Data.Add("Status", error.Status);
                            }
                            break;
                        default:
                            {
                                var error = JsonConvert.DeserializeObject<Dictionary<string, string>>(httpErrorObject);

                                if (error != null)
                                {
                                    foreach (var kvp in error)
                                    {
                                        // Wrap the errors up into the base Exception.Data Dictionary:
                                        ex.Data.Add(kvp.Key, kvp.Value);
                                    }
                                }
                            }
                            break;
                    }

                    _exception = ex;
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        public Exception Eexception => _exception;

        public string Message => _exception.Message;
    }
}
