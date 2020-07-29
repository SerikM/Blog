import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as BlogPostsStore from '../store/BlogPosts';

// At runtime, Redux will merge together...
type BlogPostsProps =
    BlogPostsStore.BlogPostsState // ... state we've requested from the Redux store
    & typeof BlogPostsStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps<{ currentBlogPost: string }>; // ... plus incoming routing parameters


class BlogPosts extends React.PureComponent<BlogPostsProps> {
    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    // This method is called when the route parameters change
    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                <h1 >My blog</h1>

                <h3>List of blogs</h3>

                {this.renderBlog()}


            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        const currentBlogPost = parseInt(this.props.match.params.currentBlogPost, 10) || 0;
        this.props.requestBlogPosts(currentBlogPost);
    }

    private renderBlog() {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Topic</th>
                        <th>Content</th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.blogPosts.map((blogPost: BlogPostsStore.BlogPost) =>
                        <tr key={blogPost.publishDate}>
                            <td>{blogPost.publishDate}</td>
                            <td>{blogPost.topic}</td>
                            <td>{blogPost.content}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    //private renderPagination() {
    //  const prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
    //  const nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

    //  return (
    //    <div className="d-flex justify-content-between">
    //      <Link className='btn btn-outline-secondary btn-sm' to={`/fetch-data/${prevStartDateIndex}`}>Previous</Link>
    //      {this.props.isLoading && <span>Loading...</span>}
    //      <Link className='btn btn-outline-secondary btn-sm' to={`/fetch-data/${nextStartDateIndex}`}>Next</Link>
    //    </div>
    //  );
    //}
}

export default connect(
    (state: ApplicationState) => state.blogPosts, // Selects which state properties are merged into the component's props
    BlogPostsStore.actionCreators // Selects which action creators are merged into the component's props
)(BlogPosts as any);
