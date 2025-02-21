import * as React from "react";
import GenericTable from "./GenericTable";

function TableService() {
  const apiEndpoint = "https://localhost:7139/api/Service";
  const columns = [
    { field: "serviceName", headerName: "Service" },
    { field: "price", headerName: "Price" },
  ];
  const inputFields = [
    { name: "serviceName", label: "Service" },
    { name: "price", label: "Price" },
  ];

  return (
    <GenericTable
      apiEndpoint={apiEndpoint}
      columns={columns}
      inputFields={inputFields}
      buttonText="Add Service"
    />
  );
}

export default TableService;
