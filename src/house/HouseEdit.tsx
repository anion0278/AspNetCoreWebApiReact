import { useParams } from "react-router-dom";
import { useFetchHouse, useUpdateHouse } from "../hooks/HouseHooks";
import ApiStatus from "../apiStatus";
import HouseForm from "./HouseForm";

const HouseEdit = () => {
    const {id} = useParams();
    if (!id) throw Error("No house ID");

    const houseId = parseInt(id);
    const {data, status, isSuccess} = useFetchHouse(houseId);
    const updateHouseMutation = useUpdateHouse();

    if (!isSuccess) return <ApiStatus status={status} />

    return <HouseForm house={data} submitted={(h) => updateHouseMutation.mutate(h)} />;
};

export default HouseEdit;