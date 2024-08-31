type Args = 
{ 
    status: "success" | "error" | "pending"; 
};

const ApiStatus = ({status} : Args) => {
    switch (status)
    {
        case "error": return <div>API error</div>
        case "pending": return <div>API loading...</div>
        default: throw Error("Unknown API state");
    }
}

export default ApiStatus;
