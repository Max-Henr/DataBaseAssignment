import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import IconButton from "@mui/material/IconButton";
import MenuIcon from "@mui/icons-material/Menu";
import ProfileButton from "../ProfileButton/ProfileButton";
import Drawer from "../Drawer/Drawer";

function ButtonAppBar() {
  return (
    <Box sx={{ flexGrow: 1, backgroundColor: "#1e293b" }}>
      <AppBar position="static">
        <Toolbar sx={{ backgroundColor: "#1e293b" }}>
          <Drawer />
          <Typography
            variant="h6"
            component="div"
            sx={{ flexGrow: 1, color: "#14b8a6" }}></Typography>
          <ProfileButton />
        </Toolbar>
      </AppBar>
    </Box>
  );
}
export default ButtonAppBar;
