import * as React from "react";
import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import Select from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import InputLabel from "@mui/material/InputLabel";
import Snackbar from "@mui/material/Snackbar";
import Alert from "@mui/material/Alert";
import { useEffect, useState } from "react";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import AddRoleButton from "../Button/AddRoleButton";

export default function ValidationTextFields({
  apiEndpoint,
  fields,
  fetchData,
  displayProperties,
  dropDownApiEndpoints,
  editItem,
}) {
  const [posts, setPosts] = useState([]);
  const initialFormData = fields.reduce(
    (acc, field) => ({ ...acc, [field.name]: "" }),
    {}
  );
  const [formData, setFormData] = React.useState(initialFormData);
  const [dropdownItems, setDropdownItems] = useState({});
  const [showNewRoleField, setShowNewRoleField] = useState(false);
  const [newRole, setNewRole] = useState("");
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState("");
  const [snackbarSeverity, setSnackbarSeverity] = useState("success");

  const statusOptions = [
    { value: 0, label: "Pending" },
    { value: 1, label: "Active" },
    { value: 2, label: "Completed" },
    { value: 3, label: "Cancelled" },
  ];

  useEffect(() => {
    if (editItem) {
      const updatedFormData = { ...editItem };
      fields.forEach((field) => {
        if (field.type === "date" && editItem[field.name]) {
          updatedFormData[field.name] = new Date(editItem[field.name]);
        }
      });
      setFormData(updatedFormData);
    }
  }, [editItem, fields]);

  const fetchDropdownItems = async () => {
    try {
      const results = await Promise.all(
        Object.keys(dropDownApiEndpoints).map((key) =>
          fetch(dropDownApiEndpoints[key]).then((res) => res.json())
        )
      );
      const items = results.reduce((acc, result, index) => {
        const key = Object.keys(dropDownApiEndpoints)[index];
        acc[key] = result;
        return acc;
      }, {});
      setDropdownItems(items);
      console.log("Dropdown items fetched:", items); // Debug log
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    fetchData();
    fetchDropdownItems();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
    console.log("Form data updated:", formData); // Debug log
  };

  const handleDateChange = (name, date) => {
    setFormData({
      ...formData,
      [name]: date,
    });
    console.log("Form data updated:", formData); // Debug log
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log("Submitting form data:", formData); // Debug log
    console.log("Edit item:", editItem); // Debug log
    try {
      const method = editItem ? "PUT" : "POST";
      const url = `${apiEndpoint}${editItem ? `/${editItem.id}` : ""}`;
      console.log(`Sending ${method} request to ${url}`); // Debug log
      const res = await fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(formData),
      });
      if (!res.ok) {
        const errorText = await res.text();
        throw new Error(`Network response was not ok: ${errorText}`);
      }
      const text = await res.text();
      const data = text ? JSON.parse(text) : {};
      console.log("Success:", data);
      fetchData();
      setFormData(initialFormData);
      setSnackbarMessage("Operation successful!");
      setSnackbarSeverity("success");
      setSnackbarOpen(true);
    } catch (error) {
      console.error("Error:", error);
      setSnackbarMessage("Operation failed!");
      setSnackbarSeverity("error");
      setSnackbarOpen(true);
    }
  };

  const handleAddRole = () => {
    setShowNewRoleField(true);
  };

  const handleNewRoleChange = (e) => {
    setNewRole(e.target.value);
  };

  const handleNewRoleSubmit = async () => {
    console.log("Adding new role:", newRole); // Debug log
    try {
      const res = await fetch("https://localhost:7139/api/role", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ roleName: newRole }), // Adjusted to match expected structure
      });
      if (!res.ok) {
        const errorText = await res.text();
        throw new Error(`Network response was not ok: ${errorText}`);
      }
      const text = await res.text();
      const data = text ? JSON.parse(text) : {};
      console.log("New role added:", data);
      setNewRole("");
      setShowNewRoleField(false);
      setDropdownItems((prevItems) => ({
        ...prevItems,
        roleId: [
          ...prevItems.roleId,
          { ...data, id: data.id || new Date().getTime() },
        ],
      }));
      fetchDropdownItems();
      setSnackbarMessage("New role added successfully!");
      setSnackbarSeverity("success");
      setSnackbarOpen(true);
    } catch (error) {
      console.error("Error adding new role:", error);
      setSnackbarMessage("Failed to add new role!");
      setSnackbarSeverity("error");
      setSnackbarOpen(true);
    }
  };

  const handleSnackbarClose = () => {
    setSnackbarOpen(false);
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <Box
        component="form"
        sx={{ "& .MuiTextField-root": { m: 1, width: "25ch" } }}
        noValidate
        autoComplete="off"
        onSubmit={handleSubmit}>
        <div>
          {fields.map((field) =>
            field.dropdown ? (
              <Box
                key={field.name}
                sx={{ display: "flex", alignItems: "center" }}>
                <FormControl sx={{ m: 1, width: "25ch" }}>
                  <InputLabel>{field.label}</InputLabel>
                  <Select
                    label={field.label}
                    name={field.name}
                    value={formData[field.name] || ""}
                    onChange={handleChange}>
                    {field.name === "status"
                      ? statusOptions.map((option) => (
                          <MenuItem key={option.value} value={option.value}>
                            {option.label}
                          </MenuItem>
                        ))
                      : dropdownItems[field.name]?.map((item) => (
                          <MenuItem
                            key={`${field.name}-${item.id}`}
                            value={item.id}>
                            {item[displayProperties[field.name]]}
                          </MenuItem>
                        )) || []}
                  </Select>
                </FormControl>
                {field.name === "roleId" && (
                  <AddRoleButton onClick={handleAddRole} />
                )}
              </Box>
            ) : field.type === "date" ? (
              <DatePicker
                key={field.name}
                label={field.label}
                value={formData[field.name] || null}
                onChange={(date) => handleDateChange(field.name, date)}
                slotProps={{
                  textField: {
                    sx: { m: 1, width: "25ch" },
                  },
                }}
              />
            ) : (
              <TextField
                key={field.name}
                label={field.label}
                name={field.name}
                value={formData[field.name]}
                onChange={handleChange}
              />
            )
          )}
          {showNewRoleField && (
            <Box sx={{ display: "flex", alignItems: "center", m: 1 }}>
              <TextField
                label="New Role"
                value={newRole}
                onChange={handleNewRoleChange}
                sx={{ width: "25ch" }}
              />
              <Button
                variant="contained"
                color="primary"
                onClick={handleNewRoleSubmit}
                sx={{ ml: 1 }}>
                Save
              </Button>
            </Box>
          )}
        </div>
        <Button type="submit" variant="contained" sx={{ m: 1 }}>
          {editItem ? "Update" : "Submit"}
        </Button>
      </Box>
      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={handleSnackbarClose}>
        <Alert
          onClose={handleSnackbarClose}
          severity={snackbarSeverity}
          sx={{ width: "100%" }}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </LocalizationProvider>
  );
}
