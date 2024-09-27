import { render, screen } from '@testing-library/react';
import FishCrud from './FishCrud';

test('renders learn react link', () => {
  render(<FishCrud />);
  const linkElement = screen.getByText(/learn react/i);
  expect(linkElement).toBeInTheDocument();
});
