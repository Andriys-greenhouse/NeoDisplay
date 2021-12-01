namespace NeoDisplay
{
    public class RootNeoObject
    {
        public Links links { get; set; }
        public Page page { get; set; }
        public Near_Earth_Objects[] near_earth_objects { get; set; }
    }

    public class Links
    {
        public string next { get; set; }
        public string self { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int total_elements { get; set; }
        public int total_pages { get; set; }
        public int number { get; set; }
    }

    public class Near_Earth_Objects
    {
        public Links1 links { get; set; }
        public string id { get; set; }
        public string neo_reference_id { get; set; }
        public string name { get; set; }
        public string name_limited { get; set; }
        public string designation { get; set; }
        public string nasa_jpl_url { get; set; }
        public float absolute_magnitude_h { get; set; }
        public Estimated_Diameter estimated_diameter { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }
        public Close_Approach_Data[] close_approach_data { get; set; }
        public Orbital_Data orbital_data { get; set; }
        public bool is_sentry_object { get; set; }

        public string LiskSelf { get { return links.self; } }
        public string FirstObservation { get { return orbital_data.first_observation_date; } }
        public string EstimatedDiametrAverage { get { return ((estimated_diameter.meters.estimated_diameter_max + estimated_diameter.meters.estimated_diameter_min) / 2).ToString(); } }
        public string EstimatedDiameterMax { get { return estimated_diameter.meters.estimated_diameter_max.ToString(); } }
        public string EstimatedDiameterMin { get { return estimated_diameter.meters.estimated_diameter_min.ToString(); } }
    }

    public class Links1
    {
        public string self { get; set; }
    }

    public class Estimated_Diameter
    {
        public Kilometers kilometers { get; set; }
        public Meters meters { get; set; }
        public Miles miles { get; set; }
        public Feet feet { get; set; }
    }

    public class Kilometers
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Meters
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Miles
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Feet
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Orbital_Data
    {
        public string orbit_id { get; set; }
        public string orbit_determination_date { get; set; }
        public string first_observation_date { get; set; }
        public string last_observation_date { get; set; }
        public int data_arc_in_days { get; set; }
        public int observations_used { get; set; }
        public string orbit_uncertainty { get; set; }
        public string minimum_orbit_intersection { get; set; }
        public string jupiter_tisserand_invariant { get; set; }
        public string epoch_osculation { get; set; }
        public string eccentricity { get; set; }
        public string semi_major_axis { get; set; }
        public string inclination { get; set; }
        public string ascending_node_longitude { get; set; }
        public string orbital_period { get; set; }
        public string perihelion_distance { get; set; }
        public string perihelion_argument { get; set; }
        public string aphelion_distance { get; set; }
        public string perihelion_time { get; set; }
        public string mean_anomaly { get; set; }
        public string mean_motion { get; set; }
        public string equinox { get; set; }
        public Orbit_Class orbit_class { get; set; }
    }

    public class Orbit_Class
    {
        public string orbit_class_type { get; set; }
        public string orbit_class_description { get; set; }
        public string orbit_class_range { get; set; }
    }

    public class Close_Approach_Data
    {
        public string close_approach_date { get; set; }
        public string close_approach_date_full { get; set; }
        public long epoch_date_close_approach { get; set; }
        public Relative_Velocity relative_velocity { get; set; }
        public Miss_Distance miss_distance { get; set; }
        public string orbiting_body { get; set; }
    }

    public class Relative_Velocity
    {
        public string kilometers_per_second { get; set; }
        public string kilometers_per_hour { get; set; }
        public string miles_per_hour { get; set; }
    }

    public class Miss_Distance
    {
        public string astronomical { get; set; }
        public string lunar { get; set; }
        public string kilometers { get; set; }
        public string miles { get; set; }
    }


}
