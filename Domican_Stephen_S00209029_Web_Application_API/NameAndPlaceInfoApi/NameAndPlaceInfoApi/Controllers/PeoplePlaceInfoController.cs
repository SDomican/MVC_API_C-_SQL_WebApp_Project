using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NameAndPlaceInfoApi.Controllers
{
    public class PeoplePlaceInfoController : ApiController
    {
        [Route("api/GetPlaceInfo/{locationName}")]
        public string Get(string locationName)
        {

            using (ApiPlaceInfoDBEntities db = new ApiPlaceInfoDBEntities())
            {

                if (db.InfoTbls.FirstOrDefault(entry => entry.InfoPlaceName.Equals(locationName)) == null) {
                    return "Entry not found!";
                }

                else if (db.InfoTbls.FirstOrDefault(entry => entry.InfoPlaceName.Equals(locationName)).Info != null)
                {
                    string returnThisToClient = db.InfoTbls.FirstOrDefault(entry => entry.InfoPlaceName.Equals(locationName)).Info;

                    return returnThisToClient;
                }

                return "Entry not found!";
            }


        }

        [Route("api/GetNames")]
        public string Get()
        {

            Random rand = new Random();

            //Gets a random first name from the API's names database and combines it with a random surname from the surname DB table and returns the concactenated string.
            using (ApiPlaceInfoDBEntities db = new ApiPlaceInfoDBEntities())
            {

                int toSkip = rand.Next(0, db.NameTbls.Count());

                string randomFirstName = db.NameTbls.OrderBy(entry => entry.FirstName).Skip(toSkip).Take(1).First().FirstName.ToString();

                int anotherSkip = rand.Next(0, db.NameTbls.Count());

                string randomSurname = db.NameTbls.OrderBy(entry => entry.LastName).Skip(anotherSkip).Take(1).First().LastName.ToString();

                string nameToReturn = randomFirstName + " " + randomSurname;

                return nameToReturn;

            }

        }


    }
}
