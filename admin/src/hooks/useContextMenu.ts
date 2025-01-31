/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect, useCallback } from "react";

export type UseContextMenu = {
    x: number;
    y: number;
    visible: boolean;
};

const useContextMenu = (constraint?: string) => {
    const [x, setX] = useState(0);
    const [y, setY] = useState(0);
    const [visible, setVisible] = useState(false);

    const handleContextMenu = useCallback(
        (e: MouseEvent) => {
            e.preventDefault();
            setX(e.clientX);
            setY(e.clientY);
            setVisible(true);
        },
        [x, y]
    );

    const handleClose = useCallback(async () => {
        if (visible) {
            await new Promise((resolve) => {
                setTimeout(() => {
                    resolve("");
                }, 200);
            });
            setVisible(false);
        }
    }, [visible]);

    useEffect(() => {
        if (constraint) {
            const listItems = document.getElementsByClassName(constraint);

            for (let i = 0; i < listItems.length; i++) {
                listItems[i].addEventListener("contextmenu", handleContextMenu as EventListener);
            }

            document.addEventListener("click", handleClose);
        } else {
            document.addEventListener("click", handleClose as EventListener);
            document.addEventListener("contextmenu", handleContextMenu);
        }

        return () => {
            document.removeEventListener("contextmenu", handleContextMenu);
            document.removeEventListener("click", handleClose);
        };
    });

    return { x, y, visible } as UseContextMenu;
};

export default useContextMenu;
