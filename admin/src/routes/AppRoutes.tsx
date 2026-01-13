import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Collection from "../pages/collection/Collection";
import SideMenu from "../components/sideMenu/SideMenu";
import Database from "../pages/database/Database";
export default function AppRoutes() {
  return (
    <Router>
      <SideMenu />
      <Routes>
        <Route path="/" element={<Database />} />
        <Route path="/:database/:collection" element={<Collection />} />
      </Routes>
    </Router>
  );
}
