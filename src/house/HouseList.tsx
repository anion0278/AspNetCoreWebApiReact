import useFetchHouses from "../hooks/HouseHooks";

const HouseList = () => {
    const houses = useFetchHouses();

    return (
        <div>
            <div className="row mb-2">
                <h5 className="themeFontColor text-center">
                    Available houses:
                </h5>
            </div>

            <table className="table talble-hover">
                <thead>
                    <tr>
                        <th>Address</th>
                        <th>Country</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        houses.map((h) =>
                        (<tr key={h.id} >
                            <td>{h.address}</td>
                            <td>{h.country}</td>
                            <td>{h.price}</td>
                        </tr>))
                    }
                </tbody>
            </table>
        </div>
    )
}

export default HouseList