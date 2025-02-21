import * as React from "react";
import GenericTable from "./GenericTable";

function TableProject() {
  const apiEndpoint = "https://localhost:7139/api/Project";
  const dropDownApiEndpoints = {
    customerId: "https://localhost:7139/api/customer",
    employeeId: "https://localhost:7139/api/employee",
    serviceId: "https://localhost:7139/api/service",
  };
  const columns = [
    { field: "projectName", headerName: "Project" },
    { field: "description", headerName: "Description" },
    { field: "startDate", headerName: "Start Date", type: "date" },
    { field: "endDate", headerName: "End Date", type: "date" },
    { field: "status", headerName: "Status" },
    { field: "customer.name", headerName: "Customer" },
    { field: "employee.firstName", headerName: "Employee" },
    { field: "service.serviceName", headerName: "Service" },
  ];
  const inputFields = [
    { name: "projectName", label: "Project" },
    { name: "description", label: "Description" },
    { name: "startDate", label: "Start Date", type: "date" },
    { name: "endDate", label: "End Date", type: "date" },
    { name: "status", label: "Status", dropdown: true },
    { name: "customerId", label: "Customer", dropdown: true },
    { name: "employeeId", label: "Employee", dropdown: true },
    { name: "serviceId", label: "Service", dropdown: true },
  ];
  const displayProperties = {
    customerId: "name",
    employeeId: "firstName",
    serviceId: "serviceName",
  };

  return (
    <GenericTable
      apiEndpoint={apiEndpoint}
      columns={columns}
      inputFields={inputFields}
      displayProperties={displayProperties}
      dropDownApiEndpoints={dropDownApiEndpoints}
      buttonText="Add Project"
      // statusOptions={statusOptions} 
    />
  );
}

export default TableProject;
