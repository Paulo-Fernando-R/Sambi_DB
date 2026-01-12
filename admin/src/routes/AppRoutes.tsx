import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Collection from "../pages/collection/Collection";
import SideMenu from "../components/sideMenu/SideMenu";
export default function AppRoutes() {
    return (
        <Router>
            <SideMenu />
            <Routes>

                <Route path="/:database/:collection" element={<Collection />} />
            </Routes>
        </Router>
    );
}
