import { useEffect } from 'react'

export default function Observer({ hasMoreToLoad, loadMore }) {
    
    useEffect(() => {
        if(loadMore && hasMoreToLoad)
            loadMore();
    }, [hasMoreToLoad, loadMore]);

    return null;
}
