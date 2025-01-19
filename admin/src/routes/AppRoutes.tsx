import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Collection from "../pages/collection/Collection";
export default function AppRoutes() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Collection />} />
            </Routes>
        </Router>
    );
}
