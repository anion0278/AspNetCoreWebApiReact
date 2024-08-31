import config from "../config";
import { House } from "../types/house";
import { useQuery } from "@tanstack/react-query";
import axios, { AxiosError } from "axios";


const useFetchHouses = () => 
{
    return useQuery<House[], AxiosError>(
        {
            queryKey: ["houses"],
            queryFn: () =>
                axios.get(`${config.baseApiUrl}/houses`).then((resp) => resp.data),
        });
};

export default useFetchHouses;
