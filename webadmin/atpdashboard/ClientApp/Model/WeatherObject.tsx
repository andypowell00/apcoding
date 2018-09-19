export class WeatherObject {
    constructor(temp_min="",temp_max="",status="",city="",description="",id="",lat=0,lng=0,temp="",windspeed="",humidity="") {
      this.dateFormatted= Date.now().toString();
      this.temp_min = temp_min;
      this.temp_max = temp_max;
      this.status = status;
      this.city = city;
      this.description = description;
      this.id=id;
      this.lat = lat;
      this.lng = lng;
      this.temp = temp;
      this.windspeed = windspeed;
      this.humidity = humidity;
    }
    dateFormatted:string;
    temp_min:string;
    temp_max:string;
    status: string;
    city: string;
    description: string;
    id:string;
    lat:number;
    lng:number;
    temp:string;
    windspeed:string;
    humidity:string
  }