import get from "@/utils/API/get";
import { useCallback, useEffect, useRef, useState } from "react";

export default function useLazyContainter(url, loadFactor, params, dataProcessor, reversed=false, numberOfnewItems=0) {
    const [items, setItems] = useState([]);

    const [hasMoreToLoad, setHasMoreToLoad] = useState(true);

    const getItems = useCallback(async (skip, take) => {
        try {
            const items = await get({
                skip,
                take,
                ...params,
            }, url);

            return items.value.data;
        }
        catch (error) {
            console.log("Error requesting new items", error?.response?.data);
            return [];
        }
    }, [url, params]);

    const addNewItems = useCallback((newItems, older = false, clear = false) => {
        if(dataProcessor)
            newItems = newItems.map(dataProcessor);

        if(clear)
            setItems(newItems);

        setItems(items => {
            const merged = (reversed && !older) || (!reversed && older) ? [
                ...items,
                ...newItems
            ] : [
                ...newItems,
                ...items
            ];

            const seen = new Set();
            return merged.filter(item => {  
                if (seen.has(item.id))
                    return false;
                seen.add(item.id);
                return true;
            })
        });
    }, [dataProcessor, reversed]);

    const addNewItem = useCallback((newItem) => {
        addNewItems([newItem]);
    }, [addNewItems]);

    const addNewItemWithUpdater = useCallback((updater) => {
        setItems(prev => updater(prev));
    }, []); 

    const loadMore = useCallback(async (skip, take) => {

        const newItems = await getItems(skip, take);
        if(newItems.length == 0)
            setHasMoreToLoad(false);

        addNewItems(newItems, true, skip === 0);

    }, [getItems, addNewItems]);


    const skipRef = useRef(0);
    const containerRef = useRef(null);
    const loadingMoreRef = useRef(false);

    useEffect(() => {
        const init = async () => {
            await loadMore(0, loadFactor);
            skipRef.current = loadFactor;
        };
        init();

    }, [loadMore, loadFactor]);

    // To be used later to display a spinning icon while loading items
    const [loadingMore, setLoadingMore] = useState(false);

    const handleLoadMore = async () => {
        if (loadingMoreRef.current) return;

        setLoadingMore(true);
        loadingMoreRef.current = true;

        await loadMore(skipRef.current + numberOfnewItems, loadFactor);
        skipRef.current += loadFactor;

        setLoadingMore(false);
        loadingMoreRef.current = false;
    };

    const handleScroll = () => {
        const el = containerRef.current;
        if (!el) return;

        if (el.scrollTop + el.clientHeight > el.scrollHeight - 1000) {
            handleLoadMore();
        }
    };

    return [items, containerRef, handleScroll, addNewItem, addNewItemWithUpdater, hasMoreToLoad];
}