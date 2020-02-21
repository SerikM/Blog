import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';


export interface BlogPostsState {
    isLoading: boolean;
    blogPosts: BlogPost[];
    currentBlogPost: number;
}

export interface BlogPost {
    date: string;
    topic: string;
    content: string;
}

interface RequestBlogPostsAction {
    type: 'REQUEST_BLOG_POSTS';
    currentBlogPost: number;
}

interface ReceiveBlogPostsAction {
    type: 'RECEIVE_BLOG_POSTS';
    blogPosts: BlogPost[];
}

type KnownAction = RequestBlogPostsAction | ReceiveBlogPostsAction;

export const actionCreators = {

    requestBlogPosts: (currentBlogPost: number): AppThunkAction<KnownAction> => (dispatch, getState) => {

        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();

        if (appState && appState.blogPosts && !appState.blogPosts.isLoading && (appState.blogPosts.currentBlogPost !== currentBlogPost || currentBlogPost === 0 && appState.blogPosts.blogPosts.length === 0)) {
            fetch(`blogpost`)
                .then(response => response.json() as Promise<BlogPost[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_BLOG_POSTS', blogPosts: data });
                });

            dispatch({ type: 'REQUEST_BLOG_POSTS', currentBlogPost: currentBlogPost});
        }
    }
};

const unloadedState: BlogPostsState = { currentBlogPost: 0, isLoading: false, blogPosts: [] };

export const reducer: Reducer<BlogPostsState> = (state: BlogPostsState | undefined, incomingAction: Action): BlogPostsState => {
    if (state === undefined) {
        return unloadedState;
    }
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_BLOG_POSTS':
            return {
                currentBlogPost: action.currentBlogPost,
                isLoading: true,
                blogPosts: state.blogPosts
            };
        case 'RECEIVE_BLOG_POSTS':
                return {
                    blogPosts: action.blogPosts,
                    isLoading: false,
                    currentBlogPost: state.currentBlogPost,
            };
        default:
        return state;
    }
};

